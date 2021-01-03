import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DataService } from '../data.service';
import { Contact } from '../contact';
 
@Component({
    selector: 'app',
    templateUrl: './modal.component.html',
    providers: [DataService],
    styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
 
    contact: Contact = new Contact();   
    
    constructor(private dataService: DataService) { }
 
    ngOnInit() {
        // this.loadContact();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadContact(id: number) {
        this.dataService.getContact(id)
            .subscribe((data: Contact) => this.contact = data);
    }
    // сохранение данных
    save() {
        /* if (this.contact.id == null) {
            this.dataService.addContact(this.contact)
                .subscribe((data: Contact[]) => this.contacts = data);
        } else {
            this.dataService.editContact(this.contact)
                .subscribe(data => this.loadContacts());
        } */
        this.cancel();
    }
    editContact(p: Contact) {
        this.contact = p;
    }
    cancel() {
        this.contact = new Contact();
    }
}