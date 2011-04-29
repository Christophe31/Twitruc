RIA technologies, Présentation
##############################

----

Que sont les RIAs
-----------------

L'acronyme RIA est issu d'un rapport de Macromedia qui a clairement été un des
initiateurs de cette tendance. Il est l'acronyme de Rich Internet Application
que l'on pourrait traduire littéralement Application Web Enrichie. Les RIA
consistent à avoir une interface dans le navigateur plus proche des interfaces
de clients lourds. Les pages webs historiquement ne changent que sur
rafraichissement. 

Ainsi, les addins ont permis d'étendre logiciellement les capacités des
navigateurs.

----

Flash
-----

Flash est une technologie Macromedia, diminutif de FutureSplash Animator, sa
sortie date de 1996 c'est à dire avant la bulle internet. Le succès de cette
technologie est lié à la simplicité de développement, les possibilités offertes
au delà du html, la légèreté du client.

En 1999, Flash est intégré par défaut dans Internet Explorer. Cette intégration
a été un évènement propice à la prospérité fulgurante de ce logiciel. Il est
désormais utilisé dans une part importante des bannières publicitaires, des jeux
en lignes, des site de diffusion de contenu multimédia. Être équipé d'un client
flash est indispensable pour beaucoup d'applications en ligne.

----

Flash
-----

Cette technologie est critiquée car contraire à l'esprit d'ouverture qui
encadre les technologies maitresses du web. La mise en exergue de ses nombreuses
failles par ses détracteurs et de ses performances assez médiocres font de
cette technologie incontournable une source de débats. Les réactions du grand
public suite au refus catégorique d'Apple à l'intégration de cette technologie
ont induit:

    #. Un regain d'intérêt pour les technologies web type HTML5
    #. Une preuve de la dépendance du grand public aux applications flash

----


Flex
----

Flex est un framework qui permet d'augmenter les perspectives de développement
pour les développeurs flash. Néanmoins, bien que Flex soit libre (Mozilla
Public License), le client cible implique acceptation de la licence d'adobe et
le compilateur est assez onéreux.

----

ActiveX
-------

ActiveX a été très critiqué plus pour ses failles de sécurités que pour ses
performances qui pour le coup sont plutôt bonnes. Cette technologie a un gros
défaut, elle fonctionne uniquement sur Microsoft Internet Explorer. ActiveX
avait pour idée de faire venir dans le navigateur les capacités de clients
lourds. Il a était inspirateur de XMLHttpRequest qui est la base d'Ajax.

ActiveX est peu utilisé en dehors des applications d'entreprises, Microsoft ne
fait plus avancer cette technologie et recommande aux utilisateurs qui n'en
n'ont pas l'usage de le désactiver pour des raisons évidentes de sécurités. Le
successeur à venir d'ActiveX pourrait être NativeClient de Google qui
permettrait l'exécution de binaires sur tous navigateurs/os.

----

Silverlight
-----------

**Points failes**:
    - Peu de personnel formé
    - Adoption moindre malgré le fort appuis de Microsoft (prive encore 
      de 20% de vos visiteurs)
    - Politique d'ouverture inefficace
    - Pas rétrocompatible ni interopérable (Windows < 2000 SP4 et Unixs 
      hors MacosX non supportés)
    - Performances moyennes
    - Intégration aux services webs fait pour .NET, bien que déployable avec 
      des technologies non Microsoft, les contraintes d'hébergements 
      réduisent grandement le public de diffuseurs de contenu.

----

Silverlight
-----------

**Points forts**:
    - Smouth streaming ( impliquant qu'un serveur IIS.)
    - Outils pour le développement amateur "gratuit"
    - Implémentation tierce reconnue et soutenue même si de piètre finition/compatibilité.
    - Réutilisation de code métier .net possible.

----

Appelets Java
-------------

Les appelets Java sont une solution multiplateforme. Son ancienneté dans
l'application d'entreprise et le nombre impressionnant de bibliothèques
existantes l'ont poussé. Il est notamment utilisé pour les transfert multiples
de fichiers, les applications réseau (IRC, Conférences web...) et applications
d'entreprise.  Néanmoins la lourdeur de la technologie et ses failles de
sécurités en font un produit aussi critiqué que flash.

----

JavaFX
------

Technologie propriétaire rivale de silverlight qui semble essayer le de
concurrencer directement en terme de fonctionnalité. Contrairement à l'appelet
java l'implémentation de référence est propriétaire malgré un début de projet
d'implémentation OpenSource (OpenJFX).

Cette technologie n'a jamais eu le succès escompté malgré le large panel
d'application entreprise en base java.

----

Javascript + XMLHttpRequest
---------------------------

Javascript est normalisé par le W3C et est implémenté par chaque navigateur. La
diversité des implémentations implique des une diversité des failles, de
performances. Les nombreuses versions et implémentations de javascript implique
une certaine complexité pour garantir que son javascript marche partout. Il
existe des navigateurs largement utilisé (ex:Internet Explorer 6) dont les
implémentations de javascript diffèrent de la norme W3C et avec des
performances modestes. Le surcout de développement pour le fonctionnement sur
l'ensemble des navigateur est non négligeable, le langage assez vieux. Le
javascript est une des technologies le mieux gérées par les navigateurs adaptés
aux déficients visuels. Il agit directement sur le html ce qui en fait une
technologie de choix pour compléter une version limité en HTML adaptée pour la
SEO. Du fait que ça n'ai pas d'attache technologiques particulière, on trouvera
javascript sur tous les navigateurs et hébergé sur tous types de serveurs.

----

Frameworks js (jquery/dojo...)
------------------------------

Si javascript permet beaucoup de chose, les contraintes d'interopérabilité en
terme de navigateurs et l'aspect limité de l'api javascript DOM native ont
encouragés l'apparition de frameworks. Ces frameworks génériques ont de
nombreux intérêts. Comme ils sont génériques, ils peuvent êtres hébergés par un
tiers (Google distribue les fichiers .js framework de nombreux sites). Une
abstraction générique permet de gagner du temps le travail de compatibilité
étant fait en dessous. L'aspect ouvert de javascript et jquery le rendent
propice à une communauté active et à l'entraide, même si le framework a des
capacités assez limités de bases des milliers de plugins sont disponibles pour
augmenter la productivité des développeurs. Autre intérêt du framework est
évidemment une documentation de qualité avec.

Pour ce qui est des contraintes de langages, il existe des surcouches de
javascript qui ne soient pas au niveau de l'api mais bien de la syntaxe, je
pense au coffeescript_ qui permet de substituer la syntaxe du javascript par une
plus familière des utilisateurs de ruby ou python, plus concise qui est
traduite en javascript pour être interprétée par les navigateurs.

.. _coffeescript: http://jashkenas.github.com/coffee-script

----

Conclusion
----------

Les technologies web ont toujours eu et ont toujours des limitations liés à des
normes restreintes en termes fonctionnels, la lenteur d'évolution naturelle des
standards W3C ont donné aux plugins une raison de vivre, ils ont permis de
mettre en exergue les attentes des utilisateurs. Des innovations majeures se sont
démocratisés via flash, activex, google gears avant d'intégrer progressivement
HTML. Conscient de cette lenteur, le comité du W3C semble aller dans la
direction d'une norme html5 qui soit évolutive et non plus fixe pour permettre
d'intégrer des innovations sans avoir à reporter son aspect stable à la
prochaine version de la norme.

Pour avoir pratiqué javascript avec dojo, je trouve personnellement que les
frameworks javascript sont une bonne solution pour allier efficacité,
interopérabilité, l'efficacité, l'intégration avec les préférences du système
et du navigateur sans imposer de contraintes à nos utilisateurs.

Flash même si mon ordinateur personnel je me passe d'activex, silverlight,
java… Flash n'est plus une option pour une navigation quotidienne.

----

S+S : hybride entre le client lourd et le client léger.
#######################################################

----

Le concept
----------

Inspiré ouvertement du SaaS (Software as a Service), S+S (Software plus
Services) consiste à déployer un client lourd pour consommer un service hébergé
sur le cloud. Ce modèle a pour avantages de permettre de décharger la
consommation de bande passante, une partie des calculs sur les postes clients,
une interface plus réactive pour les clients, la possibilité de consulter des
données sans connexions, de faire interagir localement les données du service
avec des données privées.

----

Le positionnement de Microsoft
------------------------------

**Microsoft propose un ensemble de solution pour servir transversalement toute la solution S+S**:

    - Azure pour l'hébergement cloud.
    - WCF pour développer des services d'exposition de données.
    - Silverlight pour avoir un client in browser/out of browser.
    - Intégration avec les autres technologies .NET.
