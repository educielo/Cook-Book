import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../_models';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${environment.apiEndpoint}/users`);
  }

  register(username: string, email: string, fullname: string,password: string) {
    return this.http.post<any>(`${environment.apiEndpoint}account/register`, { username, email, fullname, password })
      .pipe(map(account => {
        localStorage.setItem('currentUser', JSON.stringify(account));
        return account;
      }));
  }
}
