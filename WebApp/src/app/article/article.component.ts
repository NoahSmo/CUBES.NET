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

  constructor(
    private wineService: WineService,
    private route: ActivatedRoute,
    private imageService: ImageService,
    private commentService: CommentsService
  ) { }

  ngOnInit(): void {
    this.getArticle();
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

  getCommentsByArticleId(id: number | undefined ): Comment[] {
    let comments: Comment[] = [];
    this.comments.forEach((comment: Comment) => {
      if (comment.articleId === id) {
        comments.push(comment);
      }
    });
    return comments;
  }

}
