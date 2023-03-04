<template>
    <div v-if="stockData" class="stockInformation">
        <span class="price">{{ this.stockData.price }}</span>
        <span class="currency">{{ this.stockData.currency }}</span>
        <p>Last time checked: {{ this.stockData.date }}</p>
    </div>

    <div v-else class="stockInformation">
        <span class="price">0</span>
        <span class="currency">EUR</span>
        <p>Last time checked: Never</p>
    </div>
</template>

<script>
export default {
    name: 'SingleStock',
    props: ['stocksListDomain', 'stockNameEndpoint'],
    watch: {
        stockNameEndpoint: function(newVal, oldVal) {
            // console.log('Prop changed: ', newVal, ' | was: ', oldVal)
            this.getStockInformation(newVal)
        }
    },
    data() {
        return {
            stockData: null
        }
    },
    methods: {
        getStockInformation(customEndPoint) {
            // Call an API and find info about a new stock
            let stockEndpoint = this.stocksListDomain + '/' + customEndPoint
            fetch(stockEndpoint)
                .then((res) => res.json())
                .then((data) => this.stockData = data)
                .catch((err) => this.stockData = null)
        }
    }
}
</script>

<style scoped>
.stockInformation {
    outline-style: grsolidoove;
    background: #4d4d4d;
    border-radius: 10px;
    margin: 5px auto;
    max-width: 300px;
    color: #f1f1f1;
}

span.price {
    font-size: 50px;
    padding: 5px;
}

p {
    font-size: 10px;
    padding: 3px;
    font-weight: bold;
    font-family: Lucida Sans Typewriter,Lucida Console,monaco,Bitstream Vera Sans Mono,monospace; 
}
</style>