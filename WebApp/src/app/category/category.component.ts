import {Component, OnInit} from '@angular/core';
import {CategoryService} from "../services/category.service";
import {WineService} from "../services/wine.service";
import {Category} from "../models/category";
import {Article} from "../models/article";
import {ActivatedRoute, Router} from "@angular/router";
import {Image} from "../models/image";
import {ImageService} from "../services/image.service";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  category: Category | undefined;
  articles: Article[] = [];
  articlesByCategory: Article[] = [];
  images: Image[] = [];

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private wineService: WineService,
    private imageService: ImageService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;

    this.getCategory();

    this.imageService.getImages().subscribe((images: Image[]) => {
      this.images = images;
    });

  }

  getCategory(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.categoryService.getCategory(id).subscribe((category: Category) => {
      this.category = category;
    });
    this.wineService.getArticles().subscribe((articles: Article[]) => {
      this.articles = articles;
      this.articles = this.getArticlesByCategoryId(id);
    });
  }

  getArticlesByCategoryId(id: number): Article[] {
    this.articles.forEach((article: Article) => {
      if (article.categoryId === id) {
        this.articlesByCategory.push(article);
      }
    });
    console.log(this.articlesByCategory);
    return this.articlesByCategory;
  }

  getUrlImageByArticleId(id: number | undefined): string {
    let url: string | undefined = '';
    this.images.forEach((image: Image) => {
      if (image.articleId === id) {
        url = image.url;
      }
    });
    return url;
  }


}
