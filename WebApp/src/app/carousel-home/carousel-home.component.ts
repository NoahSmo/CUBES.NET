import { Component } from '@angular/core';

@Component({
  selector: 'app-carousel-home',
  templateUrl: './carousel-home.component.html',
  styleUrls: ['./carousel-home.component.scss']
})
export class CarouselHomeComponent {


  products: string[] = [ 'https://www.sixt.fr/magazine/wp-content/uploads//sites/3/2019/01/route-des-vins-italie.jpg', 'https://gentologie.com/wp-content/uploads/2022/04/Vin-de-Sicile-Couverture.jpg'];

  constructor() {
  }

  ngOnInit() {
  }

}
