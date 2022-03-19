from django.conf import settings
from django.conf.urls.static import static
from django.urls import include, path
from rest_framework import routers
from rest import views

router = routers.DefaultRouter()
router.register(r'users', views.UserViewSet)
router.register(r'pokemons', views.PokemonViewSet)
router.register(r'poke_profiles', views.PokeProfileViewSet)

# Wire up our API using automatic URL routing.
# Additionally, we include login URLs for the browsable API.
urlpatterns = [
    path('', include(router.urls)),
    path('poke_profiles/user/<int:pk>', views.PokeProfileView.as_view(),),
    #path('match/<int:pk>', views.MatchView.as_view(),),
    path('api-auth/', include('rest_framework.urls', namespace='rest_framework'))
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT) # To make files clickable.