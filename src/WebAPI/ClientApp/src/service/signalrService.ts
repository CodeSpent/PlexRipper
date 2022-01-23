import Log from 'consola';
import { BaseService, GlobalService } from '@service';
// eslint-disable-next-line import/named
import { LogLevel, HubConnectionBuilder, IHttpConnectionOptions, HubConnection, HubConnectionState } from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import {
	LibraryProgress,
	DownloadTaskCreationProgress,
	FileMergeProgress,
	NotificationDTO,
	DownloadTaskDTO,
	InspectServerProgress,
	SyncServerProgress,
	ServerDownloadProgressDTO,
} from '@dto/mainApi';
import { distinctUntilChanged, filter, map } from 'rxjs/operators';
import { Context } from '@nuxt/types';
import IStoreState from '@interfaces/service/IStoreState';
import { isEqual } from 'lodash';

export class SignalrService extends BaseService {
	private _progressHubConnection: HubConnection | null = null;
	private _notificationHubConnection: HubConnection | null = null;

	private _serverDownloadProgress = new Subject<ServerDownloadProgressDTO>();

	public constructor() {
		super({
			// Note: Each service file can only have "unique" state slices which are not also used in other service files
			stateSliceSelector: (state: IStoreState) => {
				return {
					downloadTaskUpdateList: state.downloadTaskUpdateList,
					libraryProgress: state.libraryProgress,
					fileMergeProgressList: state.fileMergeProgressList,
					inspectServerProgress: state.inspectServerProgress,
					syncServerProgress: state.syncServerProgress,
					notifications: state.notifications,
				};
			},
		});
	}

	public setup(nuxtContext: Context): void {
		super.setup(nuxtContext);

		GlobalService.getConfigReady().subscribe((config) => {
			Log.debug('Setting up SignalR Service');
			const options: IHttpConnectionOptions = {
				logger: LogLevel.Warning,
			};

			// Setup Connections
			const baseUrl = config.baseURL;
			this._progressHubConnection = new HubConnectionBuilder().withUrl(`${baseUrl}/progress`, options).build();
			this._notificationHubConnection = new HubConnectionBuilder().withUrl(`${baseUrl}/notifications`, options).build();

			this.setupSubscriptions();
		});
	}

	private setupSubscriptions(): void {
		this._progressHubConnection?.on('DownloadTaskUpdate', (data: DownloadTaskDTO) => {
			this.updateStore('downloadTaskUpdateList', data);
		});

		this._progressHubConnection?.on('ServerDownloadProgress', (data: ServerDownloadProgressDTO) => {
			// TODO Each subscription should work like this, every subscription here should pass its values to the designated services for that type
			this._serverDownloadProgress.next(data);
		});

		this._progressHubConnection?.on('DownloadTaskCreationProgress', (data: DownloadTaskCreationProgress) => {
			this.updateStore('downloadTaskCreationProgress', data);
		});

		this._progressHubConnection?.on('FileMergeProgress', (data: FileMergeProgress) => {
			this.updateStore('fileMergeProgressList', data);
		});

		this._progressHubConnection?.on('LibraryProgress', (data: LibraryProgress) => {
			this.updateStore('libraryProgress', data);
		});

		this._progressHubConnection?.on('InspectServerProgress', (data: InspectServerProgress) => {
			this.updateStore('inspectServerProgress', data, 'plexServerId');
		});

		this._progressHubConnection?.on('SyncServerProgress', (data: SyncServerProgress) => {
			this.updateStore('syncServerProgress', data);
		});

		this._notificationHubConnection?.on('Notification', (data: NotificationDTO) => {
			this.updateStore('notifications', data);
		});

		GlobalService.getAxiosReady().subscribe(() => {
			this.startProgressHubConnection();
			this.startNotificationHubConnection();
		});
	}

	// region Start / Stop Hub Connections

	public startProgressHubConnection(): void {
		if (this._progressHubConnection && this._progressHubConnection.state === HubConnectionState.Disconnected) {
			this._progressHubConnection.start().then(() => {
				Log.info('ProgressHub is now connected!');
			});
		}
	}

	public stopProgressHubConnection(): void {
		if (this._progressHubConnection && this._progressHubConnection.state !== HubConnectionState.Disconnected) {
			this._progressHubConnection.stop().then(() => {
				Log.info('ProgressHub is now disconnected!');
			});
		}
	}

	public startNotificationHubConnection(): void {
		if (this._notificationHubConnection && this._notificationHubConnection.state === HubConnectionState.Disconnected) {
			this._notificationHubConnection.start().then(() => {
				Log.info('NotificationHub is now connected!');
			});
		}
	}

	public stopNotificationHubConnection(): void {
		if (this._notificationHubConnection && this._notificationHubConnection.state !== HubConnectionState.Disconnected) {
			this._notificationHubConnection.stop().then(() => {
				Log.info('NotificationHub is now disconnected!');
			});
		}
	}
	// endregion

	public GetServerDownloadProgress() {
		return this._serverDownloadProgress.asObservable();
	}

	// region Array Progress
	public getAllDownloadTaskUpdate(): Observable<DownloadTaskDTO[]> {
		return this.stateChanged.pipe(
			map((x) => x?.downloadTaskUpdateList ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllServerDownloadProgress(): Observable<ServerDownloadProgressDTO[]> {
		return this.stateChanged.pipe(
			map((x) => x?.serverDownloads ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllFileMergeProgress(): Observable<FileMergeProgress[]> {
		return this.stateChanged.pipe(
			map((x) => x?.fileMergeProgressList ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllInspectServerProgress(): Observable<InspectServerProgress[]> {
		return this.stateChanged.pipe(
			map((x) => x?.inspectServerProgress ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllSyncServerProgress(): Observable<SyncServerProgress[]> {
		return this.stateChanged.pipe(
			map((x) => x?.syncServerProgress ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllLibraryProgress(): Observable<LibraryProgress[]> {
		return this.stateChanged.pipe(
			map((x) => x?.libraryProgress ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	public getAllNotifications(): Observable<NotificationDTO[]> {
		return this.stateChanged.pipe(
			map((x) => x?.notifications ?? []),
			distinctUntilChanged(isEqual),
		);
	}

	// endregion

	// region Single Progress

	public getFileMergeProgress(id: number): Observable<FileMergeProgress | null> {
		return this.getAllFileMergeProgress().pipe(
			map((x) => x?.find((x) => x.id === id) ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	public getServerDownloadProgress(id: number): Observable<ServerDownloadProgressDTO | null> {
		return this.getAllServerDownloadProgress().pipe(
			map((x) => x?.find((x) => x.id === id) ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	public getInspectServerProgress(plexServerId: number): Observable<InspectServerProgress | null> {
		return this.getAllInspectServerProgress().pipe(
			map((x) => x?.find((x) => x.plexServerId === plexServerId) ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	public getSyncServerProgress(plexServerId: number): Observable<SyncServerProgress | null> {
		return this.getAllSyncServerProgress().pipe(
			map((x) => x?.find((x) => x.id === plexServerId) ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	public getLibraryProgress(libraryId: number): Observable<LibraryProgress> {
		return this.getAllLibraryProgress().pipe(
			map((x) => x?.find((x) => x.id === libraryId) ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	public getDownloadTaskCreationProgress(): Observable<DownloadTaskCreationProgress> {
		return this.stateChanged.pipe(
			map((x) => x?.downloadTaskCreationProgress ?? null),
			filter((progress) => !!progress),
			distinctUntilChanged(isEqual),
		);
	}

	// endregion
}

const signalrService = new SignalrService();
export default signalrService;
