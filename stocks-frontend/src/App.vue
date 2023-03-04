<template>
    <nav>
        <router-link :to="{ name: 'stocks' }">Stocks</router-link> |
        <router-link :to="{ name: 'about' }">About</router-link>
    </nav>
    <router-view />
</template>

<style>
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}

nav {
    padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
  text-decoration: none;
}

nav a.router-link-exact-active {
  color: white;
  background-color: #67cf6f;
  padding: 5px;
  text-decoration: none;
  border-radius: 10px;
}
</style>
