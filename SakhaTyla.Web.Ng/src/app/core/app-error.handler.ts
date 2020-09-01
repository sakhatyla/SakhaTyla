import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
    handleError(error) {
        if (error.message) {
            console.log('error', error.message);
            if (error.stack) {
                console.log('error stack', error.stack);
            }
        } else if (typeof error === 'string') {
            console.log('error', error);
        } else {
            console.log('error', JSON.stringify(error, null, 2));
        }
    }
}
