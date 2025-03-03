import { describe, beforeAll, test, expect } from 'vitest';
import { subscribeSpyTo, baseSetup, baseVars } from '@services-test-base';
import SignalrService from '@service/signalrService';
import ISetupResult from '@interfaces/service/ISetupResult';

describe('SignalrService.setup()', () => {
	baseVars();

	beforeAll(() => {
		baseSetup();
	});

	test('Should return success and complete when setup is run', async () => {
		// Arrange
		const setup$ = SignalrService.setup();
		const setupResult: ISetupResult = {
			isSuccess: true,
			name: SignalrService.name,
		};

		// Act
		const result = subscribeSpyTo(setup$);
		await result.onComplete();

		// Assert
		expect(result.getFirstValue()).toEqual(setupResult);
		expect(result.receivedComplete()).toEqual(true);
	});
});
