<template>
  <div>
    <h1>Product</h1>
    <hr>
    <div class="product" v-for="product in allProducts">
      <router-link :to="'/shop/product/' + product.id">
      <div class="productContainer">
        <img src="https://klike.net/uploads/posts/2019-06/1560664221_1.jpg">
        <br><br>
        <strong>{{product.name}}</strong>
        <p class="price">{{product.price}} </p>
      </div>
      </router-link>
    </div>
  </div>
</template>

<script>
    export default {
        name: 'ShopProduct',
        data () {
            return {
                allProducts:[]
            }
        },
        mounted(){
            console.log("Mounted")
            this.init();
        },
        methods:{
            init(){
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.get("http://localhost:56750/api/Product/getall", ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.allProducts = res.data.data;

                    }
                })
            },
        },


    }
</script>



