import {Component, Input} from '@angular/core';
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  @Input()
  public items: MenuItem[] = [
    {
      label: 'Collection',
      icon: 'pi pi-wallet',
      items: [
        {
          label: 'Toute notre collection',
          icon: '',
        },
        {
          separator: true
        },
        {
          label: 'Vin blanc',
          icon: ''
        },
        {
          label: 'Vin rouge',
          icon: ''
        },
      ]
    },
    {
      label: 'Présentation',
      icon: 'pi pi-fw pi-pencil',
    },
    {
      label: 'Nous contacter',
      icon: 'pi pi-fw pi-user',
    },
    {
      separator: true
    }
  ];

  constructor() {
  }

  ngOnInit() {

  }

}
