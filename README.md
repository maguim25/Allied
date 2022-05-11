# Allied

Projeto contempla um Arch Clean com Principios DDD e pouco de Solid

EndPoint (cadastrar)
EndPoint (atualizar)
EndPoint (remover)

Contemplam o Json abaixo:

{
  "codigoPlano": 1,
  "minutos": 150,
  "franquiaInternet": true,
  "valor": 110,
  "tipo": 2,
  "operadora": "TIM",
  "uf": "SP"
}

EndPoint (listarPlano)
- segue com 3 parametros para localizar a lista

{
  "tipo": 2,
  "operadora": "TIM",
  "uf": "SP"
}

{
  "operadora": "TIM",
}

{
  "uf": "SP"
}

Pode ser usado somente 1 dos 3 ou 3 parametros para trazer os planos da operadora.


Foi implementado um Container de Docker no projeto.
