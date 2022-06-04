import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookAuthorshipCoreModule } from '../book-authorship-core/book-authorship-core.module';
import { BookCoreModule } from '../book-core/book-core.module';
import { BookAuthorCoreModule } from '../book-author-core/book-author-core.module';

import { BookAuthorshipListComponent } from './book-authorship-list.component';
import { BookAuthorshipEditComponent } from './book-authorship-edit.component';
import { BookAuthorshipViewComponent } from './book-authorship-view.component';
import { BookAuthorshipsComponent } from './book-authorships.component';

@NgModule({
  declarations: [
    BookAuthorshipListComponent,
    BookAuthorshipEditComponent,
    BookAuthorshipViewComponent,
    BookAuthorshipsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    BookCoreModule,
    BookAuthorCoreModule,
    BookAuthorshipCoreModule
  ],
  exports: [
    BookAuthorshipListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    BookAuthorshipEditComponent
  ]
})
export class BookAuthorshipModule {

}
