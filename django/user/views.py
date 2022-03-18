from rest_framework import viewsets
from rest_framework import permissions
from user.serializers import UserSerializer
from django.contrib.auth import get_user_model

class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """
    queryset = get_user_model().objects.all().order_by('-date_joined')
    serializer_class = UserSerializer
    permission_classes = [permissions.IsAuthenticated]
