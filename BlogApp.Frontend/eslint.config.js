const react = require('eslint-plugin-react');
const globals = require('globals');
const tsParser = require('@typescript-eslint/parser');
const prettier = require('eslint-plugin-prettier');
const typescript = require('@typescript-eslint/eslint-plugin');
const prettierConfig = require('eslint-config-prettier');

module.exports = [
    {
        files: ['**/*.{js,jsx,ts,tsx}'],
        ignores: [],
        plugins: {
            react,
            prettier,
            "@typescript-eslint": typescript
        },
        languageOptions: {
            parser: tsParser,
            parserOptions: {
                ecmaFeatures: {
                    jsx: true
                },
                ecmaVersion: 'latest',
                project: './tsconfig.json',
                sourceType: "module"
            },
            globals: {
                ...globals.browser,
                ...globals.node,
                ...globals.commonjs,
            },
        },
        rules: {
            ...typescript.configs.recommended.rules,
            ...react.configs.recommended.rules,
            ...prettierConfig.rules
        }
    }
]