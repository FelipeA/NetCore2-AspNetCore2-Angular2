import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service'; 
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  providers: [DataService]
})
export class CartPageComponent implements OnInit {

  public items: any[] = [];
  public discount: number = 0;
  public deliveryFee: number = 0;

  constructor(private cartService: CartService, private dataService: DataService, private router: Router) { }

  ngOnInit() {
    this.items = this.cartService.items;
  }

  remove(item){
    this.cartService.removeItem(item.product.id);
  }

  getSubTotal(): number{
    return this.cartService.getSubTotal();
  }

  checkQuantity(item){
    if (item.quantity < 1){
      item.quantity = 1;
    }
  }

  checkout(){
    var user = JSON.parse(localStorage.getItem('mws.user'));

    var data = {
      customerid: user.id,
      deliveryFee: this.deliveryFee,
      discount: this.discount,
      items: []
    };

    for (let i of this.cartService.items){
      data.items.push({
        productid: i.product.id,
        quantity: i.quantity
      });
    }

    this.dataService.createorder(data).subscribe(result => {
      alert('Pedido criado com sucesso!');

      this.cartService.clear();
      
      this.router.navigateByUrl('/home');
    }, error => {
      console.log(error);
    });
  }
}
