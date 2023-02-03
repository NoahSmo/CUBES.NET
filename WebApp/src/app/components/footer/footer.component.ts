import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  wineMovie() {
    window.open('https://www.youtube.com/watch?v=SfjWgLKNILQ&t=1s', '_blank');
  }

}
