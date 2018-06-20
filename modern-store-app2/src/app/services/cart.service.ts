import { Injectable } from '@angular/core'
import { Observable, Observer } from 'rxjs';
import { Jsonp } from '@angular/http';

@Injectable()
export class CartService{
    public items : any[] = [];
    cartChange: Observable<any>;
    cartChangeObserver: Observer<any>;

    constructor() 
    {
        this.cartChange = new Observable((observer: Observer<any>) => {
            this.cartChangeObserver = observer;
        });
    }

    addItem(item)
    {
        this.getItems();

        if (this.hasItem(item.product.id))
        {
            this.updateQuantity(item.product.id, 1);
        }
        else
        {
            this.items.push(item);
        }

        this.save();
        this.cartChangeObserver.next(this.items);
    }

    getItems() : any{
        var data = localStorage.getItem('mws.cart');
        if (data)
        {
            this.items = JSON.parse(data);
            this.cartChangeObserver.next(this.items);
        }

        return this.items;
    }

    updateQuantity(id, quantity){
        for (let i of this.items){
            if (i.product.id == id){
                i.quantity += quantity;
            }
        }
    }

    hasItem(id) : boolean{
        for(let i of this.items){
            if (i.product.id == id){
                return true;
            }
        }

        return false;
    }

    save(){
        localStorage.setItem('mws.cart', JSON.stringify(this.items));
    }

    load()
    {
        // var data = localStorage.getItem('mws.cart');

        // if (data)
        // {
        //     this.items = JSON.parse(data);
        //     this.cartChangeObserver.next(this.items);
        // }

        this.getItems();
    }

    removeItem(id: string)
    {
        for (var item of this.items)
        {
            if (item.product.id == id)
            {
                var index = this.items.indexOf(item);
                this.items.splice(index, 1);
            }
        }

        localStorage.setItem('mws.cart', JSON.stringify(this.items));

        this.cartChangeObserver.next(this.items);
    }

    getSubTotal(): number{
        var result: number = 0;
        for (let i of this.items){
            result += +(+i.product.price * +i.quantity);
        }
        this.cartChangeObserver.next(this.items);

        return result;
    }

    clear(){
        this.items = [];
        localStorage.removeItem('mws.cart');
        this.cartChangeObserver.next(this.items);
    }
}
