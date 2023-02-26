import {Component} from '@angular/core';
import {Article} from "../models/article";
import {WineService} from "../services/wine.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  articles: Article[] = [];

  constructor(private wineService: WineService) {
  }

  ngOnInit(): void {
    this.wineService.getArticles().subscribe((wine: Article[]) => {
      this.articles = wine;
    });
  }
}
