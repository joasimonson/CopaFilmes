module.exports = {
    verbose: true,
    preset: 'ts-jest',
    testEnvironment: 'jsdom',
    roots: ['<rootDir>/src'],

    transform: {
        '^.+\\.tsx?$': 'ts-jest'
    },

    setupFiles: ['<rootDir>/src/setupTests.ts'],
    setupFilesAfterEnv: ['@testing-library/jest-dom/extend-expect'],

    testRegex: '(/__tests__/.*|(\\.|/)(test|spec))\\.[jt]sx?$',
    moduleDirectories: ['node_modules', 'src'],
    moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx'],

    collectCoverage: true,
    coverageReporters: ['lcov'],
    coverageDirectory: 'coverage',

    collectCoverageFrom: [
        'src/**/*.{js,jsx,ts,tsx}',
        '!**/node_modules/**',
        '!src/**/*.d.ts',
        '!src/index.tsx',
        '!src/App.tsx',
        '!src/routes.tsx',
        '!src/tests/**',
        '!src/types/**'
    ]
};
