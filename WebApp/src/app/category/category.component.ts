import {Component, OnInit} from '@angular/core';
import {CategoryService} from "../services/category.service";
import {WineService} from "../services/wine.service";
import {Category} from "../models/category";
import {Article} from "../models/article";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  category: Category | undefined;
  articles: Article[] = [];

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private wineService: WineService
  ) { }

  ngOnInit(): void {
    this.getCategory();
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

  getArticlesByCategoryId(id: number | undefined ): Article[] {
    let articles: Article[] = [];
    this.articles.forEach((article: Article) => {
      if (article.categoryId === id) {
        articles.push(article);
      }
    });
    console.log(articles);
    return articles;
  }


}
