<template>
  <div>
    <modal modalHeading=" Update" :cond = "showingEditModal" @modalClose = "showingEditModal = false" >
      <table>
        <tr>
          <td>Shipment Date</td>
          <td>:</td>
          <td><input type="date" placeholder="Shipment Date" v-model="clickedOrderItems.shipmenT_DATE" ></td>
        </tr>
        <tr>
          <td>Status</td>
          <td>:</td>
          <td><select type="text" placeholder="Status" v-model="clickedOrderItems.status">
          <option>Новый</option>
            <option>Выполняется</option>
            <option>Выполнен</option>
          </select>
          </td>
          <td>
            <button class="btnSave" @click= "updateOrderItem()"> Save</button>
          </td>
        </tr>
      </table>
    </modal>

    <table class="nice-table">
      <tr ><th>Order Date</th>
        <th>Shipment Date</th>
        <th>Order Number</th>
        <th>Status</th>
      </tr>
      <tr v-for="order in orders" >
        <td>{{order.ordeR_DATE}}</td>
        <td>{{order.shipmenT_DATE}}</td>
        <td>{{order.ordeR_NUMBER}}</td>
        <td>{{order.status}}</td>
        <td><button class="edit" @click=" showingEditModal = true , clickedOrderItems = order " >Edit</button></td>
      </tr>
    </table>
  </div>
</template>
<script>
    export default {
        name: 'Users',
        data () {
            return {
                showingEditModal:false,
                orders:[],
                clickedOrderItems:{},
            }
        },
        mounted(){

            this.init();

        },
        methods:{
            init(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                let userrid = localStorage.getItem("customerid");
                let url = "http://localhost:56750/api/Order/GetAll"
                this.$axios.get(url, {headers:{ 'Authorization': userToken}} ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    console.log(res)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.orders = res.data.data;
                        console.log(this.orders)
                    }
                })

            },

            updateOrderItem(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.put("http://localhost:56750/Api/Order/", this.clickedOrderItems, {headers:{ 'Authorization': userToken}}).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                        console.log();
                        this.showingEditModal = false
                        this.init();
                    }
                    else {

                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })

            },







        },


    }
</script>



