import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class HtsClientPartnerService {

    private _url: string = './api/HtsSummary';
    private _http: HttpClient;

    constructor(http: HttpClient) {
        this._http = http;
    }

    public loadValid(): Observable<any[]> {
        return this._http.get<any>(this._url + '/partner')
            .catch(this.handleError);
    }

    public loadValidations(): Observable<any[]> {
        return this._http.get<any>(this._url + '/partnervalidations')
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('no record(s) found');
        }
        return Observable.throw(err.error);
    }

}
