module.exports = {
  overrides: [
    {
      files: ["*.component.html"],
      parser: "@angular-eslint/template-parser",
      plugins: ["@angular-eslint/template"],
      rules: {
        "@angular-eslint/template/banana-in-a-box": "error",
        "@angular-eslint/template/no-negated-async": "error",
        "@angular-eslint/template/cyclomatic-complexity": "error"
      }
    }
  ],
  env: {
    browser: true,
    es6: true,
    node: true
  },
  extends: ["plugin:@typescript-eslint/eslint-recommended", "plugin:@typescript-eslint/recommended"],
  parser: "@typescript-eslint/parser",
  parserOptions: {
    ecmaVersion: 2019,
    tsconfigRootDir: __dirname,
    project: ["./tsconfig.eslint.json"],
    sourceType: "module",
    ecmaFeatures: {
      modules: true
    }
  },
  plugins: [
    "@typescript-eslint",
    "@typescript-eslint/tslint",
    "unicorn",
    "eslint-plugin-rxjs-angular",
    "@angular-eslint/eslint-plugin",
    "rxjs",
    "rxjs-angular",
    "import"
  ],
  ignorePatterns: [
    "*.d.ts",
    "node_modules/",
    "karma.conf.js",
    "src/environments/*",
    "src/app/services/hammer-plugin-a6.patch.ts",
    "src/assets/webrtc-js/*",
    "src/polyfills.ts",
    "src/test.ts",
    "src/hmr.ts",
    "src/**/*.spec.ts",
    "src/assets/route.js",
    "src/app/components/theme/ux-theme.helper.ts"
  ],
  rules: {
    "@typescript-eslint/member-delimiter-style": "off",
    "@typescript-eslint/adjacent-overload-signatures": ["error"],
    "@typescript-eslint/class-name-casing": ["error"],
    "@typescript-eslint/consistent-type-definitions": "error",
    "@typescript-eslint/consistent-type-assertions": "off",
    "@typescript-eslint/explicit-member-accessibility": [
      "error",
      {
        accessibility: "explicit",
        overrides: {
          constructors: "no-public",
          accessors: "explicit",
          parameterProperties: "explicit"
        }
      }
    ],
    "@typescript-eslint/member-ordering": [
      "error",
      {
        default: [
          "private-static-field",
          "public-constructor",
          "public-instance-field",
          "private-instance-field",
          "public-instance-method",
          "private-instance-method"
        ]
      }
    ],
    "@typescript-eslint/no-empty-function": "off",
    "@typescript-eslint/no-empty-interface": ["error"],
    "@typescript-eslint/no-explicit-any": "error",
    "@typescript-eslint/no-for-in-array": ["error"],
    "@typescript-eslint/no-inferrable-types": "off",
    "@typescript-eslint/no-misused-new": ["error"],
    "@typescript-eslint/no-non-null-assertion": "error",
    "@typescript-eslint/no-require-imports": "error",
    "@typescript-eslint/no-this-alias": ["error"],
    "@typescript-eslint/no-var-requires": ["error"],
    "@typescript-eslint/prefer-for-of": "error",
    "@typescript-eslint/prefer-function-type": "error",
    "@typescript-eslint/require-await": ["error"],
    "@typescript-eslint/no-use-before-define": "off",
    "@typescript-eslint/semi": ["error", "always"],
    "@typescript-eslint/ban-types": ["off"],
    "@typescript-eslint/type-annotation-spacing": ["error"],
    "@typescript-eslint/unified-signatures": "error",
    "@typescript-eslint/no-useless-constructor": "error",
    "@typescript-eslint/interface-name-prefix": "off",
    "@typescript-eslint/no-misused-promises": "error",
    "@typescript-eslint/tslint/config": [
      "error",
      {
        lintFile: "./tslint.json",
        rules: {
          "template-banana-in-box": true
        }
      }
    ],
    "@typescript-eslint/ban-ts-ignore": ["warn"],
    "@typescript-eslint/camelcase": ["error"],
    "@typescript-eslint/explicit-function-return-type": ["warn"],
    "no-array-constructor": ["off"],
    "@typescript-eslint/no-array-constructor": ["error"],
    "no-empty-function": ["off"],
    "@typescript-eslint/no-namespace": ["error"],
    "no-unused-vars": ["off"],
    "@typescript-eslint/no-unused-vars": ["warn"],
    "no-use-before-define": ["off"],
    "@typescript-eslint/prefer-namespace-keyword": ["error"],
    "@typescript-eslint/triple-slash-reference": ["error"],
    "@typescript-eslint/array-type": "off",
    "@typescript-eslint/no-parameter-properties": "off",
    quotes: "off",
    "@typescript-eslint/quotes": ["error", "double", { allowTemplateLiterals: true }],

    "rxjs/no-async-subscribe": ["error"],
    "rxjs/no-create": ["error"],
    "rxjs/no-exposed-subjects": ["error"],
    "rxjs/no-ignored-replay-buffer": ["error"],
    "rxjs/no-index": ["error"],
    "rxjs/no-subject-unsubscribe": ["error"],
    "rxjs/no-unsafe-catch": ["error"],
    "rxjs/no-unsafe-first": ["error"],
    "rxjs/no-unsafe-switchmap": [
      "error",
      {
        disallow: ["add", "create", "delete", "post", "put", "remove", "set", "update"],
        observable: "action(s|\\$)?"
      }
    ],
    "rxjs/no-redundant-notify": "error",
    "rxjs/no-unsafe-takeuntil": [
      "error",
      {
        allow: [
          "count",
          "defaultIfEmpty",
          "endWith",
          "every",
          "finalize",
          "finally",
          "isEmpty",
          "last",
          "max",
          "min",
          "publish",
          "publishBehavior",
          "publishLast",
          "publishReplay",
          "reduce",
          "share",
          "shareReplay",
          "skipLast",
          "takeLast",
          "throwIfEmpty",
          "toArray"
        ]
      }
    ],

    "@angular-eslint/component-class-suffix": "error",
    "@angular-eslint/component-selector": ["error", { type: "element", prefix: "scp", style: "kebab-case" }],
    "@angular-eslint/contextual-lifecycle": "error",
    "@angular-eslint/directive-class-suffix": "error",
    "@angular-eslint/directive-selector": ["error", { type: "attribute", prefix: "scp", style: "camelCase" }],
    "@angular-eslint/no-conflicting-lifecycle": "error",
    "@angular-eslint/no-forward-ref": "error",
    "@angular-eslint/no-host-metadata-property": "error",
    "@angular-eslint/no-input-rename": "error",
    "@angular-eslint/no-inputs-metadata-property": "error",
    "@angular-eslint/no-lifecycle-call": "error",
    "@angular-eslint/no-output-native": "error",
    "@angular-eslint/no-output-rename": "error",
    "@angular-eslint/no-outputs-metadata-property": "error",
    "@angular-eslint/no-queries-metadata-property": "error",
    "@angular-eslint/use-lifecycle-interface": "error",
    "@angular-eslint/use-pipe-transform-interface": "error",
    "@angular-eslint/no-pipe-impure": "error",
    "@angular-eslint/prefer-on-push-component-change-detection": "warn",
    "@angular-eslint/use-injectable-provided-in": "error",
    "@angular-eslint/no-input-prefix": "error",
    "@angular-eslint/no-output-on-prefix": "error",
    "@angular-eslint/relative-url-prefix": "error",
    "@angular-eslint/use-component-selector": "error",
    "@angular-eslint/use-component-view-encapsulation": "error",
    "@angular-eslint/use-pipe-decorator": "error",

    "arrow-body-style": "error",
    camelcase: "error",
    "capitalized-comments": ["off"],
    "constructor-super": "error",
    curly: "error",
    "dot-notation": "off",
    "eol-last": "error",
    eqeqeq: ["error", "smart"],
    "guard-for-in": "error",
    "id-match": "error",
    "sort-imports": ["off"],
    "max-lines": ["error", 350],
    "no-bitwise": "error",
    "no-caller": "error",
    "no-cond-assign": "error",
    "no-console": [
      "off",
      {
        allow: [
          "log",
          "warn",
          "info",
          "dir",
          "timeLog",
          "assert",
          "clear",
          "count",
          "countReset",
          "group",
          "groupEnd",
          "table",
          "dirxml",
          "error",
          "groupCollapsed",
          "Console",
          "profile",
          "profileEnd",
          "timeStamp",
          "context"
        ]
      }
    ],
    "no-debugger": "error",
    "no-duplicate-case": "error",
    "no-duplicate-imports": "error",
    "no-empty": "off",
    "no-eval": "error",
    "no-extra-bind": "error",
    "no-fallthrough": "error",
    "no-invalid-this": "off",
    "no-irregular-whitespace": "error",
    "no-new-func": "error",
    "no-new-wrappers": "error",
    "no-redeclare": "error",
    "no-return-await": "error",
    "no-shadow": [
      "error",
      {
        hoist: "all"
      }
    ],
    "no-template-curly-in-string": "error",
    "no-throw-literal": "error",
    "no-trailing-spaces": "error",
    "no-undef-init": "error",
    "no-underscore-dangle": ["error", { allowAfterThis: true }],
    "no-unsafe-finally": "error",
    "no-unused-labels": "error",
    "no-useless-constructor": "off",
    "no-var": ["error"],
    "prefer-const": ["error"],
    "spaced-comment": "error",
    "unicorn/filename-case": "error",
    "use-isnan": "error",
    "no-restricted-globals": "error",
    "no-else-return": "error",
    "prefer-rest-params": ["error"],
    "prefer-spread": ["error"],
    "arrow-parens": ["off", "as-needed"],
    "comma-dangle": [
      "error",
      {
        arrays: "always-multiline",
        functions: "always-multiline",
        imports: "always-multiline",
        objects: "always-multiline"
      }
    ],
    complexity: "off",
    "import/no-deprecated": "warn",
    "import/no-unassigned-import": "off",
    "import/order": "error",
    "max-classes-per-file": "off",
    "max-len": [
      "error",
      {
        ignorePattern: "^import [^,]+ from",
        code: 140,
        ignoreComments: true,
        ignoreTemplateLiterals: true,
        ignoreStrings: true,
        ignoreTrailingComments: true
      }
    ],
    "new-parens": "error",
    "no-multiple-empty-lines": "error",
    "no-unused-expressions": [
      "error",
      {
        allowShortCircuit: true
      }
    ],
    "object-shorthand": "off",
    "one-var": ["off", "never"],
    "quote-props": ["error", "consistent-as-needed"],
    radix: "error",
    "space-before-function-paren": [
      "error",
      {
        anonymous: "never",
        asyncArrow: "always",
        named: "never"
      }
    ],
    "valid-typeof": "off"
  }
};
