export class QuestionRequste {
    Message: string;
    OkButtonText: string
    constructor(message: string, okButtonText: string = 'OK') {
        this.Message = message;
        this.OkButtonText = okButtonText;
    }
}