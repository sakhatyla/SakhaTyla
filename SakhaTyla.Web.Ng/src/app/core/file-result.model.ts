import { HttpResponse } from '@angular/common/http';

export class FileResult {
    blob: Blob;
    name: string;

    constructor(response: HttpResponse<any>) {
        const blob = new Blob([response.body as any], { type: response.headers.get('Content-Type') });
        const header = response.headers.get('Content-Disposition');
        const filenameRegex = /filename[^;=\n]*=(UTF-8'')?(([""]).*?\2|[^;\n]*)/g;
        let fileName = 'file';
        while (true) {
            const matches = filenameRegex.exec(header);
            if (!matches) {
                break;
            }
            fileName = matches[2].replace(/[""]/g, '');
            if (matches[1] === 'UTF-8\'\'') {
                fileName = decodeURIComponent(fileName);
            }
        }
        this.name = fileName;
        this.blob = blob;
    }

    download() {
        const element = document.createElement('a');
        element.href = URL.createObjectURL(this.blob);
        element.download = this.name;
        document.body.appendChild(element);
        element.click();
    }
}
