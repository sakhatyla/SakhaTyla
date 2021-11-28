import { FormGroup } from '@angular/forms';

export class Error {
  constructor(message?: string) {
    this.message = message;
  }
  message?: string;
  modelState: ModelState;
  exceptionMessage: string;
  exceptionType: string;
  errors: any[];

  httpStatus: number;

  static setFormErrors(group: FormGroup, error: Error) {
    if (error.modelState) {
      this.setFormModelStateErrors(group, error.modelState);
    }
  }

  static setFormModelStateErrors(group: FormGroup, modelState: ModelState) {
    for (const key in modelState) {
      if (group.controls[key]) {
        if (group.controls[key] instanceof FormGroup) {
          this.setFormModelStateErrors(<FormGroup>group.controls[key], <ModelState>modelState[key]);
        } else {
          const errors = (<ModelStateItem>modelState[key]).errors.reduce((o, e) => {
            o[e.errorMessage] = true;
            return o;
          }, {});
          group.controls[key].setErrors(errors);
        }
      }
    }
  }
}

export class ModelState {
  [key: string]: ModelStateItem | ModelState;
}

export class ModelStateItem {
  errors: ModelStateError[];
}

export class ModelStateError {
  errorMessage: string;
  exceptionMessage: string;
  exceptionSource: string;
}
