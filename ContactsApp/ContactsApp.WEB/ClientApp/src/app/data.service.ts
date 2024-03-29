import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Contact } from './contact';
 
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
     
    addContact(product: Contact) {
        return this.http.post(this.url, product);
    }
    editContact(product: Contact) {
        return this.http.put(this.url, product);
    }
    deleteContact(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}