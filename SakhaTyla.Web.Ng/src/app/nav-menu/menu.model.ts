export class Menu {
  items: MenuItem[];

  constructor() {
    this.items = new Array<MenuItem>();
  }
}

export class MenuItem {
  name: string;
  route: string;
  icon: string;
  roles: string[];
}
