import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http'
import { map } from 'rxjs/operators'

@Injectable()
export class DataService {
    private serviceURL: string = "http://localhost:55556/";

    constructor(private http: Http) {

    }

    createuser(data: any) {
        console.log(data);

        return this.http
            .post(this.serviceURL + 'customer', data)
            .pipe(map((res: Response) => res.json()));
    }

    authenticate(data: any) {
        var dt = "grant_type=password&username=" + data.username + "&password=" + data.password;
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.serviceURL + "/authenticate", dt, options).pipe(map((res: Response) => res.json()));
    }

    validateToken(token: string) {
        if (token || token != '')
            return true;

        return false;
    }

    getProducts()
    {
        return this.http
            .get(this.serviceURL + 'products')
            .pipe(map((res: Response) => res.json()));
    }

    createorder(data: any) {
        var token = localStorage.getItem('mws.token');

        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Authorization', `Bearer ${token}`);
        let options = new RequestOptions({ headers: headers });

        return this.http
            .post(this.serviceURL + 'order', data, options)
            .pipe(map((res: Response) => res.json()));
    }
}