import {Component, OnInit} from '@angular/core';
import {Article} from "../models/article";
import {WineService} from "../services/wine.service";
import {ImageService} from "../services/image.service";
import {Image} from "../models/image";

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {

  articles: Article[]= [];
  images: Image[] = [];

  constructor(private wineService: WineService, private imageService: ImageService) { }

  ngOnInit(): void {
    this.wineService.getArticles().subscribe((wine: Article[]) => {
      this.articles = wine;
    });

    this.imageService.getImages().subscribe((images: Image[]) => {
      this.images = images;
    });
  }


  getUrlImageByArticleId(id: number | undefined ): string {
    let url: string | undefined = '';
    this.images.forEach((image: Image) => {
      if (image.articleId === id) {
        url = image.url;
      }
    });
    return url;
  }


}
