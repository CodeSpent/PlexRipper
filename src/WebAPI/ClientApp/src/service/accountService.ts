import Log from 'consola';
import { Observable, of, combineLatest } from 'rxjs';
import { createAccount, deleteAccount, getAccount, getAllAccounts, updateAccount } from '@api/accountApi';
import { PlexAccountDTO } from '@dto/mainApi';
import { map, switchMap, tap } from 'rxjs/operators';
import { BaseService, GlobalService, SettingsService } from '@service';
import { Context } from '@nuxt/types';
import IStoreState from '@interfaces/service/IStoreState';
import ResultDTO from '@dto/ResultDTO';

export class AccountService extends BaseService {
	// region Constructor and Setup

	public constructor() {
		super({
			stateSliceSelector: (state: IStoreState) => {
				return {
					accounts: state.accounts,
				};
			},
		});
	}

	public setup(nuxtContext: Context): void {
		super.setup(nuxtContext);

		GlobalService.getAxiosReady()
			.pipe(switchMap(() => this.fetchAccounts()))
			.subscribe();
	}
	// endregion

	// region Fetch

	public fetchAccounts(): Observable<PlexAccountDTO[]> {
		return getAllAccounts().pipe(
			switchMap((accountResult) => of(accountResult?.value ?? [])),
			tap((accounts) => {
				Log.debug(`AccountService => Fetch Accounts`, accounts);
				this.setState({ accounts }, 'Fetch Accounts');
			}),
		);
	}

	public fetchAccount(accountId: Number): Observable<PlexAccountDTO | null> {
		return getAccount(accountId).pipe(
			switchMap((accountResult) => of(accountResult?.value ?? null)),
			tap((account) => {
				if (account) {
					Log.debug(`AccountService => Fetch Account`, account);
					this.updateStore('accounts', account);
				}
			}),
		);
	}

	// endregion

	public getAccounts(): Observable<PlexAccountDTO[]> {
		return this.stateChanged.pipe(switchMap((x) => of(x?.accounts ?? [])));
	}

	public getAccount(accountId: number): Observable<PlexAccountDTO | null> {
		return this.getAccounts().pipe(map((x) => x?.find((x) => x.id === accountId) ?? null));
	}

	public getActiveAccount(): Observable<PlexAccountDTO | null> {
		return combineLatest([SettingsService.getActiveAccountId(), this.getAccounts()]).pipe(
			switchMap((result: [number, PlexAccountDTO[]]) => {
				const activeAccountId = result[0];
				// Check if there is an valid account
				if (activeAccountId > 0) {
					return of(result[1].find((account) => account.id === activeAccountId) ?? null);
				}
				return of(null);
			}),
		);
	}

	/**
	 * Creates/Updates the PlexAccount and stores the new result in the store.
	 * If id is 0 then it is assumed to be an account that should be created.
	 * @param {PlexAccountDTO} account
	 * @returns {Observable<ResultDTO<PlexAccountDTO | null>>}
	 */
	public createOrUpdateAccount(account: PlexAccountDTO): Observable<ResultDTO<PlexAccountDTO | null>> {
		if (account.id === 0) {
			return createAccount(account).pipe(
				tap((createdAccount) => {
					if (createdAccount.isSuccess) {
						return this.updateStore('accounts', createdAccount.value);
					}
					Log.error(`Failed to create account ${account.displayName}`, createdAccount);
				}),
			);
		} else {
			return updateAccount(account).pipe(
				tap((createdAccount) => {
					if (createdAccount.isSuccess) {
						return this.updateStore('accounts', createdAccount.value);
					}
					Log.error(`Failed to update account ${account.displayName}`, createdAccount);
				}),
			);
		}
	}

	public deleteAccount(accountId: number) {
		return deleteAccount(accountId).pipe(switchMap(() => this.fetchAccounts()));
	}
}

const accountService = new AccountService();
export default accountService;
