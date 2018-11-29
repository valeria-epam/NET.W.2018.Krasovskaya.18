1. В текстовом файле построчно хранится информация о URL-адресах,
представленных в виде &lt;scheme&gt;://&lt;host&gt;/&lt;URL‐path&gt;?&lt;parameters&gt;, где сегмент
parameters - это набор пар вида key=value, при этом сегменты URL‐path и parameters
или сегмент parameters могут отсутствовать.
Разработать систему типов (руководствоваться принципами SOLID) для
экспорта данных, полученных на основе разбора информации текстового файла, в
XML-документ по следующему правилу, например, для текстового файла с URL-
адресами

https://github.com/AnzhelikaKravchuk?tab=repositories

https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU

https://habrahabr.ru/company/it-grad/blog/341486/

результирующим является XML-документ вида (можно использовать любую XML
технологию без ограничений).
