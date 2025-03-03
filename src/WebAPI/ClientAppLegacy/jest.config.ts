/*
 * For a detailed explanation regarding each configuration property and type check, visit:
 * https://jestjs.io/docs/configuration
 */

import { pathsToModuleNameMapper } from 'ts-jest/utils';

const { compilerOptions } = require('./tsconfig');

const jestConfig = {
	// A preset that is used as a base for Jest's configuration
	preset: 'ts-jest',

	// Automatically clear mock calls, instances, contexts and results before every test
	clearMocks: true,

	// Indicates whether the coverage information should be collected while executing the test
	collectCoverage: true,

	// An array of glob patterns indicating a set of files for which coverage information should be collected
	collectCoverageFrom: ['<rootDir>/components/**/*.vue', '<rootDir>/pages/**/*.vue'],

	// The directory where Jest should output its coverage files
	coverageDirectory: 'coverage',

	// A map from regular expressions to module names or to arrays of module names that allow to stub out resources with a single module
	// Source: https://stackoverflow.com/a/68190596/8205497
	moduleNameMapper: pathsToModuleNameMapper(compilerOptions.paths, { prefix: '<rootDir>/src' }),
	moduleFileExtensions: ['ts', 'js', 'vue', 'json'],
	transform: {
		'^.+\\.ts$': 'ts-jest',
		'^.+\\.js$': 'babel-jest',
		'.*\\.(vue)$': 'vue-jest',
	},
	modulePaths: [compilerOptions.baseUrl],
	// A list of paths to directories that Jest should use to search for files in
	roots: ['<rootDir>'],
	// An array of regexp pattern strings that are matched against all source file paths, matched files will skip transformation
	// transformIgnorePatterns: ['/node_modules/', '\\.pnp\\.[^\\/]+$'],
	// Source: https://stackoverflow.com/a/43197503/8205497
	transformIgnorePatterns: ['<rootDir>/node_modules/(?!lodash-es)(?!axios)'],

	// A set of global variables that need to be available in all test environments
	// NOTE: window is needed due to a check for window.cypress to check if it is running under tests

	// The test environment that will be used for testing
	// Error: "ReferenceError: window is not defined" when running tests
	// Fix: https://github.com/kulshekhar/ts-jest/discussions/2670#discussioncomment-841239
	testEnvironment: 'jsdom',

	// Indicates whether each individual test should be reported during the run
	verbose: true,

	// A list of paths to modules that run some code to configure or set up the testing framework before each test
	// Source: https://github.com/hirezio/observer-spy#-configuring-jest-with-setup-auto-unsubscribejs
	setupFilesAfterEnv: ['<rootDir>/node_modules/@hirez_io/observer-spy/dist/setup-auto-unsubscribe.js'],

	// The paths to modules that run some code to configure or set up the testing environment before each test
	// Fixes the use of Url.createObjectURL()
	// Source: https://github.com/developit/jsdom-worker1
	setupFiles: ['jsdom-worker'],

	// All imported modules in your tests should be mocked automatically
	// automock: false,

	// Stop running tests after `n` failures
	// bail: 0,

	// The directory where Jest should store its cached dependency information
	// cacheDirectory: "/tmp/jest_rs",

	// An array of regexp pattern strings used to skip coverage collection
	// coveragePathIgnorePatterns: [
	//   "/node_modules/"
	// ],

	// Indicates which provider should be used to instrument code for coverage
	// coverageProvider: "babel",

	// A list of reporter names that Jest uses when writing coverage reports
	// coverageReporters: [
	//   "json",
	//   "text",
	//   "lcov",
	//   "clover"
	// ],

	// An object that configures minimum threshold enforcement for coverage results
	// coverageThreshold: undefined,

	// A path to a custom dependency extractor
	// dependencyExtractor: undefined,

	// Make calling deprecated APIs throw helpful error messages
	// errorOnDeprecated: false,

	// The default configuration for fake timers
	// fakeTimers: {
	//   "enableGlobally": false
	// },

	// Force coverage collection from ignored files using an array of glob patterns
	// forceCoverageMatch: [],

	// A path to a module which exports an async function that is triggered once before all test suites
	// globalSetup: undefined,

	// A path to a module which exports an async function that is triggered once after all test suites
	// globalTeardown: undefined,

	// The maximum amount of workers used to run your tests. Can be specified as % or a number. E.g. maxWorkers: 10% will use 10% of your CPU amount + 1 as the maximum worker number. maxWorkers: 2 will use a maximum of 2 workers.
	// maxWorkers: "50%",

	// An array of directory names to be searched recursively up from the requiring module's location
	// moduleDirectories: [
	//   "node_modules"
	// ],

	// An array of file extensions your modules use
	// moduleFileExtensions: [
	//   "js",
	//   "mjs",
	//   "cjs",
	//   "jsx",
	//   "ts",
	//   "tsx",
	//   "json",
	//   "node"
	// ],

	// An array of regexp pattern strings, matched against all module paths before considered 'visible' to the module loader
	// modulePathIgnorePatterns: [],

	// Activates notifications for test results
	// notify: false,

	// An enum that specifies notification mode. Requires { notify: true }
	// notifyMode: "failure-change",

	// Run tests from one or more projects
	// projects: undefined,

	// Use this configuration option to add custom reporters to Jest
	// reporters: undefined,

	// Automatically reset mock state before every test
	// resetMocks: false,

	// Reset the module registry before running each individual test
	// resetModules: false,

	// A path to a custom resolver
	// resolver: undefined,

	// Automatically restore mock state and implementation before every test
	// restoreMocks: false,

	// The root directory that Jest should scan for tests and modules within
	// rootDir: undefined,

	// Allows you to use a custom runner instead of Jest's default test runner
	// runner: "jest-runner",

	// The number of seconds after which a test is considered as slow and reported as such in the results.
	// slowTestThreshold: 5,

	// A list of paths to snapshot serializer modules Jest should use for snapshot testing
	// snapshotSerializers: [],

	// Options that will be passed to the testEnvironment
	// testEnvironmentOptions: {},

	// Adds a location field to test results
	// testLocationInResults: false,

	// The glob patterns Jest uses to detect test files
	// testMatch: [
	//   "**/__tests__/**/*.[jt]s?(x)",
	//   "**/?(*.)+(spec|test).[tj]s?(x)"
	// ],

	// An array of regexp pattern strings that are matched against all test paths, matched tests are skipped
	// testPathIgnorePatterns: [
	//   "/node_modules/"
	// ],

	// The regexp pattern or array of patterns that Jest uses to detect test files
	// testRegex: [],

	// This option allows the use of a custom results processor
	// testResultsProcessor: undefined,

	// This option allows use of a custom test runner
	// testRunner: "jest-circus/runner",

	// A map from regular expressions to paths to transformers
	// transform: undefined,

	// An array of regexp pattern strings that are matched against all modules before the module loader will automatically return a mock for them
	// unmockedModulePathPatterns: undefined,

	// An array of regexp patterns that are matched against all source file paths before re-running tests in watch mode
	// watchPathIgnorePatterns: [],

	// Whether to use watchman for file crawling
	// watchman: true,
};

export default jestConfig;
