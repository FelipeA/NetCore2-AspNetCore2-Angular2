import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable()
export class AuthService implements CanActivate{
    
    constructor(private router: Router) {
        
    }

    canActivate(){
        if (!localStorage.getItem('mws.token'))
        {
            this.router.navigateByUrl('/');
            return false;
        }

        return true;
    }
}