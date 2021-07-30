import { DialogController } from 'aurelia-dialog';
import { QuestionRequste } from '../models/questionrequste';
import { inject } from 'aurelia-framework';
@inject(DialogController)
export class Question {
    controller: DialogController;
    questionRequest: QuestionRequste;
    constructor(controller: DialogController) {
        this.controller = controller;
        this.questionRequest = new QuestionRequste('');
    }
    activate(questionRequest: QuestionRequste) {
        this.questionRequest.Message = questionRequest.Message;
        this.questionRequest.OkButtonText = questionRequest.OkButtonText;
    }
}