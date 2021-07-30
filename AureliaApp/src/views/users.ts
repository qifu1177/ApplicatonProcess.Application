import { UserObject } from '../models/userobject';
import { DataGrid } from '../uibases/datagrid';
export class Users extends DataGrid<UserObject>{

    activate() {
        this.parentId = '';
        this.routeName = 'user';
        this.controlName = 'Users';
        this.parentRouteName = '';
        this.childRouteName = 'assets';        
    }

    override updateDatas(data) {
        this.datas.splice(0,this.datas.length);
        for(let item of data){
            this.datas.push(new UserObject(item.age,item.firstName,item.lastName,item.address,item.email));
        }
    }
}