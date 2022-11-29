import Vue from 'vue';
import Vuetify from 'vuetify';
import { Context } from '@nuxt/types';
import { Inject } from '@nuxt/types/app';
import '@mdi/font/css/materialdesignicons.css';
import { UserVuetifyPreset } from 'vuetify/types/services/presets';

Vue.use(Vuetify);
/*
 ** vuetify module configuration
 ** https://github.com/nuxt-community/vuetify-module
 */
export default (ctx: Context, inject: Inject): void => {
	const vuetify = new Vuetify(vuetifyConfigOptions);

	ctx.app.vuetify = vuetify;
	ctx.$vuetify = vuetify.framework;

	inject('isDark', ctx.$vuetify.theme.dark);
	inject('getThemeClass', (): String => (ctx.$vuetify.theme.dark ? 'theme--dark' : 'theme--light'));
};

export const vuetifyConfigOptions: Partial<UserVuetifyPreset> = {
	customVariables: ['~/assets/scss/_variables.scss'],
	icons: {
		iconfont: 'mdi',
	},
	theme: {
		dark: true,
		options: {
			customProperties: true,
		},
		themes: {
			light: {
				primary: '#ff0000',
			},
			dark: {
				primary: '#ff0000',
			},
		},
	},
};
