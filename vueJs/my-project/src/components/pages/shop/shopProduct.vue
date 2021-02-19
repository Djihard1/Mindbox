<template>
  <div>
    <h1 class="fleft">{{product.name}}</h1>
    <router-link to="/shop/products" class="fright" > All Product</router-link>
    <div class="clear">
    </div>
    <hr>
    <div class="product-left">
      <img src="https://klike.net/uploads/posts/2019-06/1560664221_1.jpg">
      <br>
    </div>
    <div class="product-right">
      <div class="product-description">
        <p>
          <strong>Id:</strong> {{product.id}} <br><br>
          <strong>Code:</strong> {{product.code}} <br><br>
          <strong>Name:</strong> {{product.name}} <br><br>
          <strong>Price:</strong> {{product.price}} <br><br>
          <strong>Category:</strong> {{product.category}} <br><br>
        </p>
        <strong>Quantity:</strong>
        <input type="number" class="input-number" v-model="qty">
        <button class="addBtn" @click="addToCart()"> Add to cart</button>
      </div>
    </div>
    <div class="clear"></div>
    <hr> <br><br><br>
    <p class="txt-center">
      <router-link to="/shop/products" class="addBtn"> Continue shopping</router-link>
    </p>
  </div>
</template>

<script>
    export default {
        name: 'ShopProduct',
        data () {
            return {
                productId:0,
                product:{},
                qty:1

            }
        },
        mounted(){
            this.productId = this.$route.params.pid
            this.init();
        },
        methods:{
            init(){
                console.log(this.productId)
                let url = 'http://localhost:56750/api/product/' + this.productId
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.get(url ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.product = res.data.data;
                       // console.log(this.product)
                    }
                })
            },
            addToCart(){
                this.$eventBus.$emit("addToCart", {product: this.product, quality:this.qty})
            }
        },
    }
</script>



