import { Directive, TemplateRef, Input } from '@angular/core';

@Directive({
  selector: '[appEntityChangeValue]'
})
export class EntityChangeValueDirective {

  constructor(public template: TemplateRef<any>) { }

  @Input()
  public appEntityChangeValueName: string;
}
