import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { OwlDateTimeModule, OwlMomentDateTimeModule, OWL_DATE_TIME_FORMATS } from '@danielmoncada/angular-datetime-picker';
import { MaterialModule } from '../material.module';

import { ErrorInterceptor } from './error.interceptor';
import { LoadingInterceptor } from './loading.interceptor';

import { EnumKeysPipe } from './pipes/enumkeys.pipe';
import { ModelErrorPipe } from './pipes/model-error.pipe';

import { TextEditComponent } from './controls/text.edit.component';
import { TextViewComponent } from './controls/text.view.component';
import { NumberEditComponent } from './controls/number.edit.component';
import { NumberViewComponent } from './controls/number.view.component';
import { BoolEditComponent } from './controls/bool.edit.component';
import { BoolViewComponent } from './controls/bool.view.component';
import { DateTimeEditComponent } from './controls/datetime.edit.component';
import { DateTimeViewComponent } from './controls/datetime.view.component';
import { DateEditComponent } from './controls/date.edit.component';
import { DateViewComponent } from './controls/date.view.component';
import { TimeEditComponent } from './controls/time.edit.component';
import { TimeViewComponent } from './controls/time.view.component';

import { LoadingService } from './loading.service';
import { StoreService } from './store.service';

import { ModalComponent } from './modal.component';
import { ModalHelper } from './modal.helper';
import { AppErrorHandler } from './app-error.handler';
import { ClickOutsideDirective } from './directives/click-outside.directive';
import { NoticeHelper } from './notice.helper';
import { SortActivePipe } from './pipes/sort-active.pipe';
import { SortDirectionPipe } from './pipes/sort-direction.pipe';
import { SelectItemDirective } from './directives/select-item.directive';
import { SelectAllItemsDirective } from './directives/select-all-items.directive';

export const MY_MOMENT_FORMATS = {
  parseInput: 'L LT',
  fullPickerInput: 'L LT',
  datePickerInput: 'L',
  timePickerInput: 'LT',
  monthYearLabel: 'MMM YYYY',
  dateA11yLabel: 'LL',
  monthYearA11yLabel: 'MMMM YYYY',
};

@NgModule({
  declarations: [
    EnumKeysPipe,
    ModelErrorPipe,
    SortActivePipe,
    SortDirectionPipe,
    ClickOutsideDirective,
    SelectItemDirective,
    SelectAllItemsDirective,
    ModalComponent,
    TextEditComponent,
    TextViewComponent,
    NumberEditComponent,
    NumberViewComponent,
    BoolEditComponent,
    BoolViewComponent,
    DateTimeEditComponent,
    DateTimeViewComponent,
    DateEditComponent,
    DateViewComponent,
    TimeEditComponent,
    TimeViewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    OwlDateTimeModule,
    OwlMomentDateTimeModule,
    MaterialModule
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: OWL_DATE_TIME_FORMATS, useValue: MY_MOMENT_FORMATS },
    StoreService,
    LoadingService,
    ModalHelper,
    NoticeHelper,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  entryComponents: [
    ModalComponent
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    EnumKeysPipe,
    ModelErrorPipe,
    SortActivePipe,
    SortDirectionPipe,
    ClickOutsideDirective,
    SelectItemDirective,
    SelectAllItemsDirective,
    TextEditComponent,
    TextViewComponent,
    NumberEditComponent,
    NumberViewComponent,
    BoolEditComponent,
    BoolViewComponent,
    DateTimeEditComponent,
    DateTimeViewComponent,
    DateEditComponent,
    DateViewComponent,
    TimeEditComponent,
    TimeViewComponent
  ]
})
export class CoreModule {

}
