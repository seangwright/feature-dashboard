export interface IJQueryModule {
  run: () => void;
  ready: ($: JQueryStatic) => void;
}
