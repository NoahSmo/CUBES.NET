import {Component, Input} from '@angular/core';
import {MenuItem} from "primeng/api";
import {style} from "@angular/animations";

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
          routerLink: '/collections'
        },
        {
          separator: true
        },
        {
          label: 'Vin blanc',
          icon: '',
          routerLink: '/category/2'
        },
        {
          label: 'Vin rouge',
          icon: '',
          routerLink: '/category/1'
        },
        {
          label: 'Rosés',
          icon: '',
          routerLink: '/category/3'
        },
        {
          label: 'Champagne',
          icon: '',
          routerLink: '/category/4'
        },
      ]
    },
    {
      label: 'Présentation',
      icon: 'pi pi-fw pi-pencil',
      routerLink: '/presentation'
    },
    {
      label: 'Nous contacter',
      icon: 'pi pi-fw pi-user',
      command: () => {
        window.scrollTo({
          top: 9999999,
          behavior: 'smooth'
        });
      }
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
