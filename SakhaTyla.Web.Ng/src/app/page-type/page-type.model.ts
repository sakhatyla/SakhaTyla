export enum PageType {
  General = 0,
  Blog = 1,
  Article = 2,
  Main = 3
}

const PageTypeDisplay: { [index: number]: string } = {};
PageTypeDisplay[PageType.General] = 'General';
PageTypeDisplay[PageType.Blog] = 'Blog';
PageTypeDisplay[PageType.Article] = 'Article';
PageTypeDisplay[PageType.Main] = 'Main';
export { PageTypeDisplay };
