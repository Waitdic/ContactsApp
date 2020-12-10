import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Product } from './product';
 
@Injectable()
export class DataService {
 
    private url = "/api/contacts";
 
    constructor(private http: HttpClient) {
    }
 
    getContacts() {
        return this.http.get(this.url);
    }
     
    getContact(id: number) {
        return this.http.get(this.url + '/' + id);
    }
     
    addContact(product: Product) {
        return this.http.post(this.url, product);
    }
    editContact(product: Product) {
  
        return this.http.put(this.url, product);
    }
    deleteContact(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}