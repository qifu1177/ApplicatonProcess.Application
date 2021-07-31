import { HttpClient } from 'aurelia-fetch-client';
export async function jsonLoader(url:string) {
    let httpClient:HttpClient=new HttpClient();
    let data:any;
    await httpClient.fetch(url)
            .then(async response => {
                data= await response.json();                
            })
            .catch(error => {
                console.log(error);
            });
    return data;
}