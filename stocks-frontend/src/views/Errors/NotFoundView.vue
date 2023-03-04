<template>
    <div class="error">
        <h1>404</h1>
        <p>Page not found</p>
        <p>😔</p>
    </div>
</template>

<script>
export default {
    name: 'NotFoundView'
}
</script>

<style scoped>
.error {
    max-width: 200px;
    align-items: center;
    display: block;
    margin-left: auto;
    margin-right: auto;
}

h1 {
    color: black;
    font-size: 5rem;
    margin: 5px;
}

p {
    font-size: 1.5rem;
    margin: 2px;
}


</style>