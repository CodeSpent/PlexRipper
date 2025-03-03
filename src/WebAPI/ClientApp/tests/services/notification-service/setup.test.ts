import { describe, beforeAll, beforeEach, test, expect } from 'vitest';
import { subscribeSpyTo, baseSetup, getAxiosMock, baseVars } from '@services-test-base';
import NotificationService from '@service/notificationService';
import { generateResultDTO } from '@mock';
import { NOTIFICATION_RELATIVE_PATH } from '@api-urls';
import ISetupResult from '@interfaces/service/ISetupResult';

describe('NotificationService.setup()', () => {
	let { mock } = baseVars();

	beforeAll(() => {
		baseSetup();
	});

	beforeEach(() => {
		mock = getAxiosMock();
	});

	test('Should return success and complete when setup is run', async () => {
		// Arrange
		mock.onGet(NOTIFICATION_RELATIVE_PATH).reply(200, generateResultDTO([]));
		const setup$ = NotificationService.setup();
		const setupResult: ISetupResult = {
			isSuccess: true,
			name: NotificationService.name,
		};

		// Act
		const result = subscribeSpyTo(setup$);
		await result.onComplete();

		// Assert
		expect(result.getFirstValue()).toEqual(setupResult);
		expect(result.receivedComplete()).toEqual(true);
	});
});
