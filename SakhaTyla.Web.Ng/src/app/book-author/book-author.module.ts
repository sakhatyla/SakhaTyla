import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookAuthorCoreModule } from '../book-author-core/book-author-core.module';

import { BookAuthorListComponent } from './book-author-list.component';
import { BookAuthorEditComponent } from './book-author-edit.component';
import { BookAuthorViewComponent } from './book-author-view.component';
import { BookAuthorsComponent } from './book-authors.component';

@NgModule({
  declarations: [
    BookAuthorListComponent,
    BookAuthorEditComponent,
    BookAuthorViewComponent,
    BookAuthorsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    BookAuthorCoreModule
  ],
  exports: [
    BookAuthorListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    BookAuthorEditComponent
  ]
})
export class BookAuthorModule {

}
