[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)

# Introdução

Local Storage utiliza `ScriptableObjects` para tornar o acesso à arquivos mais facil e pratico. Este package conta com:

- Armazenamento de arquivos no disco.
- Criptografia.
- Cache de arquivos (exclusão automatica após x segundos, minutos, horas, dias).

## Instalação

Para instalar, abra o **Unity Package Manager** e adicione o pacote git:

# Como contribuir

Se você encontrou algum bug, tem alguma sugestão ou dúvida, crie uma issue aqui no github. Caso queira contribuir com código, faça um fork do projeto e siga as boas praticas abaixo, e faça um pull request.

## Convenção de namespace

Para isolar os assets de outros scripts isolamos todos no namespace do package `HGS.<package-name>`. Neste package usamos `HGS.LocalStorage`.

## Branchs

Este package conta com duas branchs principais:

- `master` -> Aqui guardamos todo material do projeto.
- `upm` -> Aqui mantemos uma copia do package que se encontra na pasta `Assets/Package`.

Sempre que um merge é feito na branch `unity`, o script de CI irá criar uma copia da subpasta `Assets/Package` automaticamente na branch `upm`.

## Convenção de commit

Utilizamos o plugin [semantic-release](https://github.com/semantic-release/semantic-release) para facilitar o sistema de release e versionamento, portanto, precisamos seguir com a seguinte convenção de commit:

```
<type>(<scope>): <short summary>
  │       │             │
  │       │             └─⫸ Breve descrição do que foi feito
  │       │
  │       └─⫸ Scope: Namespace, nome do script, etc..
  │
  └─⫸ Commit Type: build|ci|docs|feat|fix|perf|refactor|test
```

`Type`.:

- build: Alterações que afetam o sistema de compilação ou dependências externas (escopos de exemplo: sistema de pacotes)
- ci: Alterações em arquivos e scripts de configuração de CI (escopos de exemplo: Circle, - BrowserStack, SauceLabs)
- docs: Apenas mudanças na documentação
- feat: Um novo recurso
- fix: Uma correção de bug
- perf: Uma mudança de código que melhora o desempenho
- refactor: Uma alteração de código que não corrige um bug nem adiciona um recurso
- test: Adicionando testes ausentes ou corrigindo testes existentes
