import Log from 'consola';
import Axios from 'axios-observable';
import { GlobalService } from '@service';
import IAppConfig from '@class/IAppConfig';

export default defineNuxtPlugin({
	name: 'plex-ripper-boot',
	enforce: 'post',
	hooks: {
		'app:created'() {
			const publicEnv = useRuntimeConfig().public;
			Log.level = 4;
			// Log.level = config.public.isProduction ? LogLevel.Debug : LogLevel.Debug;
			Log.info(`Nuxt Environment: ${publicEnv.version}`);

			let baseUrl = `http://localhost:${publicEnv.apiPort}`;
			if (publicEnv.isDocker) {
				const currentLocation = window.location;
				baseUrl = `${currentLocation.protocol}//${currentLocation.hostname}:${currentLocation.port}`;
			}

			const appConfig: IAppConfig = {
				version: publicEnv.version,
				nodeEnv: publicEnv.nodeEnv,
				isProduction: publicEnv.nodeEnv === 'production',
				isDocker: publicEnv.isDocker,
				baseUrl,
			};

			setupAxios(appConfig);

			GlobalService.setupServices(appConfig);
		},
	},
});

function setupAxios(appConfig: IAppConfig) {
	Axios.defaults.baseURL = appConfig.baseUrl + '/api';

	// Source: https://github.com/axios/axios/issues/41#issuecomment-484546457
	Axios.defaults.validateStatus = () => true;
	// Now error resolves in catch block rather than then block.
	// Source: https://github.com/axios/axios/issues/41#issuecomment-386762576
	Axios.interceptors.response.use(
		(config) => {
			return config;
		},
		(error) => {
			// Prevents 400 & 500 status code throwing exceptions
			return Promise.reject(error);
		},
	);
}
