import {Component, OnInit} from '@angular/core';
import {Article} from "../models/article";
import {WineService} from "../services/wine.service";
import {ActivatedRoute} from "@angular/router";
import {ImageService} from "../services/image.service";
import {Image} from "../models/image";
import {Comment} from "../models/comment";
import {CommentsService} from "../services/comments.service";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {

  article: Article | undefined;
  image: Image | undefined;
  comments: Comment[] = [];
  number: number = 1;
  quantity: number | undefined;
  currentQuantity: number | undefined;

  constructor(
    private wineService: WineService,
    private route: ActivatedRoute,
    private imageService: ImageService,
    private commentService: CommentsService
  ) {
  }

  ngOnInit(): void {
    this.getArticle();
    // this.changeColorByQuantity();
  }

  getArticle(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.wineService.getArticle(id).subscribe((article: Article) => {
      this.article = article;
      this.article.comments = this.getCommentsByArticleId(id);
      console.log(article.comments);
    });
    this.imageService.getImage(id).subscribe((image: Image) => {
      this.image = image;
    });
    this.commentService.getComments().subscribe((comments: Comment[]) => {
      this.comments = comments;
    });
  }

  getCommentsByArticleId(id: number | undefined): Comment[] {
    let comments: Comment[] = [];
    this.comments.forEach((comment: Comment) => {
      if (comment.articleId === id) {
        comments.push(comment);
      }
    });
    return comments;
  }

  quantityDelete() {
    this.quantity = this.article?.stock;
    if (this.quantity) {
      this.currentQuantity = this.quantity + 1;
    }
    if (this.number - 1 < 0) {
      this.number = 0;
    } else {
      this.number = this.number - 1;
    }
  }

  quantityAdd() {
    this.quantity = this.article?.stock;
    if (this.quantity) {
      this.currentQuantity = this.quantity - 1;
    }
    this.number = this.number + 1;
  }

  // changeColorByQuantity() {
  //
  //   const htmlElement = document.getElementsByClassName("p-card p-card-subtitle") as HTMLCollectionOf<HTMLElement>;
  //   const quantity = this.article?.stock;
  //
  //   if (quantity) {
  //     for (let i = 0; i < htmlElement.length; i++) {
  //       if (quantity < 50) {
  //         htmlElement[i].style.color = "red"; // Si le nombre est supérieur à 50, le texte sera en vert
  //       } else if (quantity >= 50 && quantity <= 100) {
  //         htmlElement[i].style.color = "orange"; // Si le nombre est inférieur à 50, le texte sera en rouge
  //       } else if (quantity >= 100) {
  //         htmlElement[i].style.color = "green"; // Si le nombre est égal à 50, le texte sera en noir
  //       }
  //     }
  //   }
  //
  // }

}
