export enum WidgetType {
  Html = 0
}

const WidgetTypeDisplay: { [index: number]: string } = {};
WidgetTypeDisplay[WidgetType.Html] = 'Html';
export { WidgetTypeDisplay };
