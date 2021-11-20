import Log from 'consola';
import { Context } from '@nuxt/types';
import AppConfig from '@class/AppConfig';
import { ReplaySubject, Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { ObservableStoreSettings } from '@codewithdan/observable-store/interfaces';
import { ObservableStore } from '@codewithdan/observable-store';
import { DownloadTaskCreationProgress, SettingsModelDTO, ViewMode } from '@dto/mainApi';
import IStoreState from '@interfaces/service/IStoreState';
import * as Service from '@service';
import { RuntimeConfig } from '~/type_definitions/vueTypes';

export class GlobalService extends Service.BaseService {
	private _axiosReady: ReplaySubject<void> = new ReplaySubject();
	private _configReady: ReplaySubject<AppConfig> = new ReplaySubject();
	private _defaultStore: IStoreState = {
		accounts: [],
		servers: [],
		libraries: [],
		mediaUrls: [],
		notifications: [],
		folderPaths: [],
		alerts: [],
		helpIdDialog: '',
		settings: {} as SettingsModelDTO,
		downloadTaskUpdateList: [],
		// Progress Service
		fileMergeProgressList: [],
		inspectServerProgress: [],
		syncServerProgress: [],
		libraryProgress: [],
		downloadTaskCreationProgress: {} as DownloadTaskCreationProgress,
		serverDownloads: [],
		// Settings
		activeAccountId: 0,
		firstTimeSetup: true,
		downloadSegments: 4,
		askDownloadMovieConfirmation: true,
		askDownloadTvShowConfirmation: true,
		askDownloadSeasonConfirmation: true,
		askDownloadEpisodeConfirmation: true,
		tvShowViewMode: ViewMode.Poster,
		movieViewMode: ViewMode.Poster,
		shortDateFormat: 'dd/MM/yyyy',
		longDateFormat: 'EEEE, dd MMMM yyyy',
		timeFormat: 'HH:MM:ss',
		timeZone: 'UTC',
		showRelativeDates: true,
		language: 'en-US',
	};

	constructor() {
		super({} as ObservableStoreSettings);
	}

	public setAxiosReady(): void {
		Log.info('Axios is ready');
		this._axiosReady.next();
	}

	public setup(nuxtContext: Context): void {
		super.setup(nuxtContext);

		ObservableStore.initializeState(this._defaultStore);

		for (const key of Object.keys(Service)) {
			if (key === 'BaseService' || key === 'GlobalService') {
				continue;
			}
			if (Service[key] && typeof Service[key].setup === 'function') {
				Service[key].setup(nuxtContext);
			}
		}
	}

	public resetStore(): void {
		ObservableStore.resetState(this._defaultStore);
	}

	public setConfigReady(config: RuntimeConfig): void {
		if (process.client || process.static) {
			Log.info('Runtime Config is ready - ' + config.version, config);
			this._configReady.next(new AppConfig(config));
		} else {
			Log.error('setConfigReady => Process was neither client or static, was:', process);
		}
	}

	public getAxiosReady(): Observable<void> {
		return this._axiosReady.pipe(take(1));
	}

	public getConfigReady(): Observable<AppConfig> {
		return this._configReady.pipe(take(1));
	}
}

const globalService = new GlobalService();
export default globalService;
