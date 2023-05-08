# OnlineShopApi



 - Api para cadastro de Produtos e vendas online



## Pre-requisitos

<br>

- Visual Studio 2019/2022
- .Net Core 6.0
- MongoDb

<br>

## Schemas

- Exemplo Json para Cadastro de produto

```
{
  "nome": "string",
  "preco": 0,
  "descricao": "string"
}
```
<br>

- Exemplo Json para buscar produtos

```
{
  "nome": "string",
  "preco": 0,
  "descricao": "string"
}
```

<br>

- Exemplo Json para criar carrinho de compras

```
{
  "produtos": [
    "string"
  ]
}
```

<br>

- Exemplo retorno de carrinho de compras

```
{
  "id": "string",
  "produtos": [
    {
      "id": "string",
      "nome": "string",
      "preco": 0,
      "descricao": "string",
      "dataCadastro": "2023-05-08T20:35:29.528Z",
      "produtoVendido": true
    }
  ],
  "precoTotal": 0
}
```

<br>

- Exemplo Json para realizar venda

```
{
  "carrinhoId": "string",
  "cupomDesconto": 0
}
```

<br>

- Exemplo Retorno de vendas

```
[
  {
    "id": "string",
    "dataVenda": "2023-05-08T20:36:50.531Z",
    "carrinho": [
      {
        "id": "string",
        "produtos": [
          {
            "id": "string",
            "nome": "string",
            "preco": 0,
            "descricao": "string",
            "dataCadastro": "2023-05-08T20:36:50.531Z",
            "produtoVendido": true
          }
        ],
        "precoTotal": 0
      }
    ],
    "cupomDesconto": 0,
    "valorFinal": 0
  }
]
```


