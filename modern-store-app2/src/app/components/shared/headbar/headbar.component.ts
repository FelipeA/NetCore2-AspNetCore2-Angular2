import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-headbar',
  templateUrl: './headbar.component.html'
})
export class HeadbarComponent implements OnInit {

  public totalItems: number = 0;
  public user: string = "";

  constructor(private cartService: CartService, private router: Router) { 
     this.cartService.cartChange.subscribe((data) => {
       this.totalItems = data.length;
     });

     var userData = localStorage.getItem('mws.user');

     if (userData){
       this.user = JSON.parse(userData).name;
     }

     this.cartService.load();
  }

  logout(){
    localStorage.removeItem('mws.user');
    localStorage.removeItem('mws.token');
    this.router.navigateByUrl("/");
  }

  // addItem(){
  //   this.cartService.addItem({title: '123'});
  // }

  ngOnInit() {
  }

}
